using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Customers_DAL.Entities;
using Customers_DAL.UnitOfWork.Abstract;
using Customers_BLL.DTO.Requests;
using Customers_BLL.DTO.Responses;
using Customers_BLL.Exceptions;
using Customers_BLL.Factories.Abstract;
using Customers_BLL.Helpers;
using Customers_BLL.Services.Abstract;
using Customers_DAL.Exceptions;
using Customers_DAL.Helpers;
using Customers_DAL.Repositories.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

namespace Customers_BLL.Services.Concrete
{
    public class IdentityService : IIdentityService
    {
        private readonly IUnitOfWork unitOfWork;

        private readonly IMapper mapper;

        private readonly ICustomerRepository customerRepository;

        private readonly IEmployeeRepository employeeRepository;

        private readonly IBarberRepository barberRepository;
        
        private readonly UserManager<User> userManager;

        private readonly IJwtSecurityTokenFactory tokenFactory;
        
        private readonly IHttpContextAccessor accessor;
        
        private readonly LinkGenerator generator;
        
        private readonly IEmailSender emailSender;

        public IdentityService(IUnitOfWork unitOfWork, IMapper mapper, IJwtSecurityTokenFactory tokenFactory, IHttpContextAccessor accessor, LinkGenerator generator, IEmailSender emailSender)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            
            customerRepository = this.unitOfWork.CustomerRepository;
            employeeRepository = this.unitOfWork.EmployeeRepository;
            barberRepository = this.unitOfWork.BarberRepository;
            
            userManager = unitOfWork.UserManager;
            this.tokenFactory = tokenFactory;
            this.accessor = accessor;
            this.generator = generator;
            this.emailSender = emailSender;
        }

        public async Task<JwtResponse> Login(UserLoginRequest request)
        {
            var user = await userManager.FindByEmailAsync(request.Email)
                       ?? throw new EntityNotFoundException(nameof(User), request.Email);

            if (!await userManager.CheckPasswordAsync(user, request.Password))
                throw new ForbiddenAccessException("Incorrect username or password.");
            
            if(!await userManager.IsEmailConfirmedAsync(user))
                throw new ForbiddenAccessException("Please confirm your email to log in");
            
            return await PrepareToken(user);
        }

        private async Task<User> RegisterUser<T>(T request) where T : BaseRegisterRequest 
        {
            var user = mapper.Map<T, User>(request);
            var signUpResult = await userManager.CreateAsync(user, request.Password);

            if (signUpResult.Succeeded) 
                return user;
                
            string errors = string.Join("\n",
                signUpResult.Errors.Select(error => error.Description));

            throw new ArgumentException(errors);
        }

        public async Task<string> RegisterCustomer(CustomerRegisterRequest request)
        {
            var user = await RegisterUser(request);
            
            await userManager.AddToRoleAsync(user, UserRoles.Customer);

            var customer = mapper.Map<CustomerRegisterRequest, Customer>(request);
            customer.UserId = user.Id;
            await customerRepository.InsertAsync(customer);
            await unitOfWork.SaveChangesAsync();
            
            return await SendVerificationEmail(user);
        }

        public async Task<string> RegisterBarber(BarberRegisterRequest request)
        {
            var user = await RegisterUser(request);
            
            await userManager.AddToRoleAsync(user, UserRoles.Barber);

            var barber = mapper.Map<BarberRegisterRequest, Barber>(request);
            barber.EmployeeUserId = user.Id;
            await barberRepository.InsertAsync(barber);
            await unitOfWork.SaveChangesAsync();
            
            return await SendVerificationEmail(user);
        }
        
        public async Task<string> RegisterManager(EmployeeRegisterRequest request)
        {
            var user = await RegisterUser(request);
            
            await userManager.AddToRoleAsync(user, UserRoles.Manager);

            var employee = mapper.Map<EmployeeRegisterRequest, Employee>(request);
            employee.UserId = user.Id;
            await employeeRepository.InsertAsync(employee);
            await unitOfWork.SaveChangesAsync();

            return await SendVerificationEmail(user);
        }
        
        private async Task<JwtResponse> PrepareToken(User user)
        {
            var roles = await userManager.GetRolesAsync(user);
            var jwtToken = tokenFactory.BuildToken(user, roles);
            string serializedToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return new JwtResponse { Token = serializedToken };
        }

        private async Task<string> SendVerificationEmail(User user)
        {
            var confirmToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = generator.GetUriByAction(
                accessor.HttpContext,
                action: "ConfirmEmail",
                controller: "Identity",
                values: new { userId = user.Id, confirmToken = confirmToken} );
            
            var emails = new List<string> { user.Email };
            var message = new MessageModel {
                To = emails,
                Subject = "Confirm your email",
                Content = "<h1>Welcome to Barbershop London</h1>" +
                         $"<p>Please confirm your email by <a href='{callbackUrl}'>Clicking here</a></p>"
            };
            
            await emailSender.SendEmailAsync(message);
            return "The message was successfully sent to your email!";
        }

        public async Task<string> ConfirmEmail(int userId, string? confirmToken)
        {
            if (userId == 0 || confirmToken == null)
            {
                throw new NoNullAllowedException("User id and confirmation token can not be null");
            }
            
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new ForbiddenAccessException($"Unable to load user with ID '{userId}'.");
            }
            var result = await userManager.ConfirmEmailAsync(user, confirmToken);
            return result.Succeeded ? "Successfully confirmed" : "Error";
        }

        public async Task<string> ForgotPasswordAsync(string email)
        {
            var user = await userManager.FindByEmailAsync(email) 
                       ?? throw new EntityNotFoundException(nameof(User), email);

            var resetToken = await userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = Encoding.UTF8.GetBytes(resetToken);
            var validToken = WebEncoders.Base64UrlEncode(encodedToken);
            
            var callbackUrl = $"http://localhost:3000/resetPass?userId={user.Id}&confirmToken={validToken}";
            
            var emails = new List<string> { user.Email };
            var message = new MessageModel {
                To = emails,
                Subject = "Reset Password",
                Content = "<h1>Follow the instructions to reset your password</h1>" +
                          $"<p>To reset your password <a href='{callbackUrl}'>Click here</a></p>"
            };

            await emailSender.SendEmailAsync(message);
            return "Reset password URL has been sent to the email successfully!";
        }

        public async Task<string> ResetPasswordAsync(ResetPasswordRequest request)
        {
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == request.UserId)
                       ?? throw new EntityNotFoundException(nameof(User), request.UserId);
            
            var decodedToken = WebEncoders.Base64UrlDecode(request.Token);
            string normalToken = Encoding.UTF8.GetString(decodedToken);
            
            var result = await userManager.ResetPasswordAsync(user, normalToken, request.NewPassword);

            if (!result.Succeeded)
                throw new Exception("Something went wrong!");

            return "Password has been reset successfully!";
        }
    }
}

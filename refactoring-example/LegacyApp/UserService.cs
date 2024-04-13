using System;

namespace LegacyApp
{
    public class UserService(IClientRepository clientRepository, IUserCreditService userCreditService)
    {
        public bool AddUser(string firstName, string lastName, string email, DateTime dateOfBirth, int clientId)
        {
            if (Validate(firstName, lastName, email, dateOfBirth) == false)
            {
                return false;
            }

            var client = clientRepository.GetById(clientId);

            var user = new User
            {
                Client = client,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                FirstName = firstName,
                LastName = lastName
            };

            if (user.HasCreditLimit)
            {
                user.CreditLimit = userCreditService.GetCreditLimit(user);
            }
            
            if (user.CreditLimit < 500)
            {
                return false;
            }

            UserDataAccess.AddUser(user);
            return true;
        }

        private bool Validate(string firstName, string lastName, string email, DateTime dateOfBirth)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                return false;
            }

            if (email.Contains('@') == false || email.Contains('.') == false)
            {
                return false;
            }

            if (GetAge(dateOfBirth) < 21)
            {
                return false;
            }
            
            return true;
        }
        
        private static int GetAge(DateTime dateOfBirth)
        {
            var now = DateTime.Now;
            var age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;
           
            return dateOfBirth < now.AddYears(-age) ? age : age - 1;
        }
    }
}

using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace HospitalAutomation.Utilities
{
    public static class ValidationHelper
    {
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            try
            {
                // Normalize the domain
                email = Regex.Replace(email, @"(@)(.+)$", DomainMapperMethod,
                                    RegexOptions.None, TimeSpan.FromMilliseconds(200));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }

        private static string DomainMapperMethod(Match match)
        {
            // Use IdnMapping class to convert Unicode domain names.
            var idn = new IdnMapping();

            // Pull out and process domain name (throws ArgumentException on invalid)
            var domainName = idn.GetAscii(match.Groups[2].Value);

            return match.Groups[1].Value + domainName;
        }

        public static bool IsValidPhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return false;

            // Remove all non-digit characters
            var digits = Regex.Replace(phone, @"\D", "");

            // Turkish phone number validation
            // Should be 10 digits (without country code) or 12 digits (with +90)
            return digits.Length == 10 || (digits.Length == 12 && digits.StartsWith("90"));
        }

        public static bool IsValidPhoneNumber(string phone)
        {
            return IsValidPhone(phone);
        }

        public static bool IsValidTCKimlikNo(string tcKimlikNo)
        {
            if (string.IsNullOrWhiteSpace(tcKimlikNo) || tcKimlikNo.Length != 11)
                return false;

            // Check if all characters are digits
            if (!tcKimlikNo.All(char.IsDigit))
                return false;

            // TC Kimlik No cannot start with 0
            if (tcKimlikNo[0] == '0')
                return false;

            // TC Kimlik No algorithm
            var digits = tcKimlikNo.Select(c => int.Parse(c.ToString())).ToArray();

            // Sum of first 10 digits
            var sum = 0;
            for (int i = 0; i < 10; i++)
            {
                sum += digits[i];
            }

            // 11th digit should be sum % 10
            if (sum % 10 != digits[10])
                return false;

            // Sum of odd positioned digits (1st, 3rd, 5th, 7th, 9th)
            var oddSum = digits[0] + digits[2] + digits[4] + digits[6] + digits[8];
            
            // Sum of even positioned digits (2nd, 4th, 6th, 8th)
            var evenSum = digits[1] + digits[3] + digits[5] + digits[7];

            // 10th digit should be ((oddSum * 7) - evenSum) % 10
            if (((oddSum * 7) - evenSum) % 10 != digits[9])
                return false;

            return true;
        }

        public static bool IsValidAge(int age)
        {
            return age >= 0 && age <= 150;
        }

        public static bool IsValidPassword(string password, int minLength = 6)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            return password.Length >= minLength;
        }

        public static bool IsValidName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return false;

            // Name should contain only letters, spaces, and Turkish characters
            return Regex.IsMatch(name, @"^[a-zA-ZçÇðÐýÝöÖþÞüÜ\s]+$");
        }

        public static string NormalizePhoneNumber(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
                return string.Empty;

            // Remove all non-digit characters
            var digits = Regex.Replace(phone, @"\D", "");

            // Add country code if missing
            if (digits.Length == 10)
            {
                digits = "90" + digits;
            }

            // Format as +90 (5xx) xxx xx xx
            if (digits.Length == 12 && digits.StartsWith("90"))
            {
                return $"+90 ({digits.Substring(2, 3)}) {digits.Substring(5, 3)} {digits.Substring(8, 2)} {digits.Substring(10, 2)}";
            }

            return phone; // Return original if format is not recognized
        }

        public static string FormatTCKimlikNo(string tcKimlikNo)
        {
            if (string.IsNullOrWhiteSpace(tcKimlikNo) || tcKimlikNo.Length != 11)
                return tcKimlikNo;

            // Format as xxx xxx xxx xx
            return $"{tcKimlikNo.Substring(0, 3)} {tcKimlikNo.Substring(3, 3)} {tcKimlikNo.Substring(6, 3)} {tcKimlikNo.Substring(9, 2)}";
        }
    }
}
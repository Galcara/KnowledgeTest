using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLogicalLayer
{
    public static class StringExtensions
    {
        public static string AuthenticateName(this string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return "Nome deve ser informado.";
            }
            if (name.Length < 3 || name.Length > 50)
            {
                return "Nome deve ter entre 3 e 50 caracteres.";
            }
            return "";
        }
        public static string AuthenticateCPF(this string cpf)
        {
            int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string tempCpf;
            string digito;
            int soma;
            int resto;
            cpf = cpf.Trim();
            cpf = cpf.Replace(".", "").Replace("-", "");
            if (cpf.Length != 11)
                return "CPF deve conter 11 caracteres.";
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCpf += digito;
            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito += resto.ToString();
            bool val = cpf.EndsWith(digito);
            if (val)
            {
                return "";
            }
            return "CPF inválido.";
        }
        public static string AuthenticateCNPJ(this string cnpj)
        {
            int[] multiplicador1 = new int[12] { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = new int[13] { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma;
            int resto;
            string digito;
            string tempCnpj;
            cnpj = cnpj.Trim();
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
            if (cnpj.Length != 14)
                return "CNPJ deve conter 14 caracteres.";
            tempCnpj = cnpj.Substring(0, 12);
            soma = 0;
            for (int i = 0; i < 12; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = resto.ToString();
            tempCnpj = tempCnpj + digito;
            soma = 0;
            for (int i = 0; i < 13; i++)
                soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];
            resto = (soma % 11);
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;
            digito = digito + resto.ToString();
            bool val = cnpj.EndsWith(digito);
            if (val)
            {
                return "";
            }
            return "CNPJ inválido.";
        }
        public static string AuthenticatePhoneNumber(this string phonenumber)
        {
            if (string.IsNullOrWhiteSpace(phonenumber))
            {
                return "Telefone inválido.";
            }
            else if (phonenumber.Length > 11 && phonenumber.Length < 9)
            {
                return "Telefone inválido.";
            }
            return "";
        }
        public static string AuthenticateEmail(this string email)
        {
            Regex regex = new Regex(@"\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}");
            if (regex.IsMatch(email))
            {
                return "";
            }
            return "Email inválido";
        }
        public static string RemoveMask(this string masked)
        {
            masked = masked.Replace(".", "").Replace("-", "").Replace("/", "");
            return masked;
        }
        public static string RemoveMaskPhoneNumber(this string phonenumber)
        {
            phonenumber = phonenumber.Replace("(", "").Replace(")", "").Replace("-", "");
            return phonenumber;
        }
    }
}

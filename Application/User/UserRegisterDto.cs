namespace Application.User
{
    public class UserRegisterDto
    {
        /// <summary>
        /// User name
        /// </summary>
        /// <example>tester</example>
        public string UserName { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        /// <example>test@test.pl</example>
        public string Email { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        ///<example>!Password1</example>
        public string Password { get; set; }
    }
}

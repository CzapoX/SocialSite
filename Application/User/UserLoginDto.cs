namespace Application.User
{
    public class UserLoginDto
    {
        /// <summary>
        /// User email
        /// </summary>
        ///<example>norbert@test.pl</example>
        public string Email { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        ///<example>!Password1</example>
        public string Password { get; set; }
    }
}

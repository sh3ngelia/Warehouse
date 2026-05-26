using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Users
{
    public class PersonRegistrationDto
    {
        public required string PersonalId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Phone { get; set; }
        public required string Email { get; set; }
        public string Username { get; set; } = default!;
        public byte[] Password { get; set; } = default!;
    }
}

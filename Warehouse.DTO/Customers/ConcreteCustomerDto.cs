using System;

namespace Warehouse.DTO.Customers
{
    public enum CustomerType
    {
        Physical,
        Legal
    }

    public enum CustomerStatus
    {
        Active,
        Deleted
    }

    public class ConcreteCustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CustomerType Type { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PersonalId { get; set; }
        public DateTime Created { get; set; }
        public CustomerStatus Status { get; set; }

        public ConcreteCustomerDto(int id, string name, CustomerType type, string phone, string email, string personalId, DateTime created, CustomerStatus status)
        {
            Id = id;
            Name = name;
            Type = type;
            Phone = phone;
            Email = email;
            PersonalId = personalId;
            Created = created;
            Status = status;
        }

        public ConcreteCustomerDto() { }

        public string GetInitials()
        {
            var parts = Name.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2)
                return $"{parts[0][0]}{parts[1][0]}".ToUpper();
            return Name.Length >= 2 ? Name.Substring(0, 2).ToUpper() : Name.ToUpper();
        }
    }
}

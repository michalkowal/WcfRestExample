using System.Runtime.Serialization;

namespace WcfRestExample.Common.Data
{
    /// <summary>
    /// Employee entity - WCF Data Contract
    /// </summary>
    [DataContract]
    public class Employee
    {
        /// <summary>
        /// Employee Identifier - WCF Data Member (required field)
        /// </summary>
        [DataMember(IsRequired = true)]
        public int EmployeeID { get; set; }

        /// <summary>
        /// Employee's name - WCF Data Member (required field)
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Name { get; set; }

        /// <summary>
        /// Employee's address - WCF Data Member (required field)
        /// </summary>
        [DataMember(IsRequired = true)]
        public string Address { get; set; }

        /// <summary>
        /// Employee's email - WCF Data Member
        /// </summary>
        [DataMember(IsRequired = false)]
        public string Email { get; set; }

        /// <summary>
        /// /// <summary>
        /// Employee's phone number - WCF Data Member
        /// </summary>
        /// </summary>
        [DataMember(IsRequired = false)]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Print all properties with values
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format(
                "{{ EmployeeID={0}, " +
                "Name={1}, " +
                "Address={2}, " +
                "Email={3}, " +
                "PhoneNumber={4} }}",
                EmployeeID,
                Name ?? string.Empty,
                Address ?? string.Empty,
                Email ?? string.Empty,
                PhoneNumber ?? string.Empty);
        }
    }
}

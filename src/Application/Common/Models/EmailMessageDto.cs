using System;
using System.Collections.Generic;
using System.Text;

namespace NanoCell.Application.Common.Models
{
    public class EmailAddressDto
    {
        private string _name;

        public string Name
        {
            get
            {
                if (string.IsNullOrEmpty(_name))
                {
                    return Address;
                }
                return _name;
            }
            set
            {
                _name = value;
            }
        }
        public string Address { get; set; }

        public EmailAddressDto(string name, string address)
        {
            _name = name;
            Address = address;
        }
    }
    public class EmailMessageDto
    {
        public EmailMessageDto()
        {
            ToAddresses = new List<EmailAddressDto>();
            FromAddresses = new List<EmailAddressDto>();
            CcAddresses = new List<EmailAddressDto>();
            BccAddresses = new List<EmailAddressDto>();
        }

        public List<EmailAddressDto> ToAddresses { get; set; }
        public List<EmailAddressDto> FromAddresses { get; set; }
        public List<EmailAddressDto> BccAddresses { get; set; }
        public List<EmailAddressDto> CcAddresses { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsHtml { get; set; } = true;
    }
}

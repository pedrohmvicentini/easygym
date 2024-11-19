using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    public class Notifies
    {
        public Notifies()
        {
            Notifications = new List<Notifies>();
        }

        [NotMapped]
        public string PropertyName { get; set; } = string.Empty;
        [NotMapped]
        public string Message { get; set; } = string.Empty;
        [NotMapped]
        public List<Notifies> Notifications { get; set; }


        public bool ValidateStringValue(string value, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(propertyName))
            {
                Notifications.Add(new Notifies { 
                    Message = "Campo obrigatório",
                    PropertyName = propertyName 
                });

                return false;
            }
            return true;
        }

        public bool ValidateIntValue(int value, string propertyName)
        {
            if (value < 1 || string.IsNullOrWhiteSpace(propertyName))
            {
                Notifications.Add(new Notifies
                {
                    Message = "Campo obrigatório",
                    PropertyName = propertyName
                });

                return false;
            }
            return true;
        }
    }
}

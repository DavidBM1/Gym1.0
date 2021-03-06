//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Gym5DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class PersonaGym
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public PersonaGym()
        {
            this.PersonaMedidasGyms = new HashSet<PersonaMedidasGym>();
        }
    
        public string idPersona { get; set; }
        public string nombre { get; set; }
        public string apellido1 { get; set; }
        public string apellido2 { get; set; }
        public string telefono { get; set; }
        public Nullable<System.DateTime> fechaPago { get; set; }
        public string idInstructor { get; set; }
        public string enfermedad { get; set; }
        public string observaciones { get; set; }
        public string idNombreEmpresa { get; set; }
    
        public virtual InstructorGym InstructorGym { get; set; }
        public virtual LogEmpresa LogEmpresa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonaMedidasGym> PersonaMedidasGyms { get; set; }
    }
}

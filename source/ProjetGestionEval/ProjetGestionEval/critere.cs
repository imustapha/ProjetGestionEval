//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ProjetGestionEval
{
    using System;
    using System.Collections.Generic;
    
    public partial class critere
    {
        public critere()
        {
            this.evaluation = new HashSet<evaluation>();
        }
    
        public int IDCRITERE { get; set; }
        public int IDFAMILLECRITERE { get; set; }
        public string NOMCRITERE { get; set; }
        public string NoteCritere { get; set; }
    
        public virtual famillecritere famillecritere { get; set; }
        public virtual ICollection<evaluation> evaluation { get; set; }
    }
}

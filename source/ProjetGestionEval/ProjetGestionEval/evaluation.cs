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
    
    public partial class evaluation
    {
        public evaluation()
        {
            this.variable = new HashSet<variable>();
            this.critere = new HashSet<critere>();
        }
    
        public int IDEVALUATION { get; set; }
        public int IDEVALUATEUR { get; set; }
        public int IDCOLLABORATEUR { get; set; }
        public Nullable<System.DateTime> DATEEVALUATION { get; set; }
        public Nullable<System.DateTime> DATENEXTEVALUATION { get; set; }
        public string APPRECIATION { get; set; }
        public string STATUT { get; set; }
        public string Memo { get; set; }
    
        public virtual collaborateur collaborateur { get; set; }
        public virtual collaborateur collaborateur1 { get; set; }
        public virtual ICollection<variable> variable { get; set; }
        public virtual ICollection<critere> critere { get; set; }
    }
}

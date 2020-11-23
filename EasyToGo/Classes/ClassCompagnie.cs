using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyToGo.Classes
{
    class ClassCompagnie
    {
        public string Code { get; set; }
        public string Noms { get; set; }
        public string Description { get; set; }
        public string Adresse { get; set; }
        public byte[] photo { get; set; }
        public string Rccm { get; set; }
        public string Email { get; set; }
        public string Mot_de_passe { get; set; }
        public string Ref_POS { get; set; }
        public decimal Commission { get; set; }
        public decimal refSolde { get; set; }

        public string Etat { get;set;}


    }
}

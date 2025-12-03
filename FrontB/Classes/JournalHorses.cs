using System;
namespace FrontB.Classes
{
   public class JournalHorses
    {

        public JournalHorses(int? _counter, string _guid, string _lakamy, int? _doglanyyl, string _atasy,
            string _enesi, string _jynsy, string _renki,string _biomaterial, int? _biosan, 
            string _probnomer, string _eyesi, string _nyshanlar,string _bellik)
        {   
            this.Counter = _counter;
            this.Guid = _guid;
            this.Lakamy = _lakamy;
            this.Doglanyyl = _doglanyyl;
            this.Atasy = _atasy;
            this.Enesi = _enesi;
            this.Jynsy = _jynsy;
            this.Renki = _renki;
            this.Biomaterial = _biomaterial;
            this.Biosan = _biosan;
            this.Probnomer = _probnomer;   
            this.Eyesi = _eyesi;
            this.Nyshanlar = _nyshanlar;
            this.Bellik = _bellik;            
        }               
        public int? Counter { get; set; }
        public string Guid { get; set; }    
        public string Lakamy { get; set; }
        public int? Doglanyyl { get; set; }
        public string Atasy { get; set; }
        public string Enesi { get; set; }
        public string Jynsy { get; set; }
        public string Renki { get; set; }        
        public string Biomaterial { get; set; }
        public int? Biosan { get; set; }
        public string Probnomer { get; set; }
        public string Eyesi { get; set; }
        public string Nyshanlar { get; set; }
        public string Bellik { get; set; }
    }
}
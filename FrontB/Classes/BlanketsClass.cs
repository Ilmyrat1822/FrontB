namespace FrontB.Classes
{    
    public class BlanketsClass
    {
        public BlanketsClass(int? _counter, string? _guid, string? _ykrarhat, string? _ysene, int? _san, int? _atsan,string? _belllik)

        {
            this.Counter = _counter;
            this.Guid = _guid;
            this.Ykrarhat = _ykrarhat;
            this.Ysene = _ysene;
            this.San = _san;
            this.Atsan = _atsan;
        }
        public int? Counter { get; set; }
        public string? Guid { get; set; }
        public string? Ykrarhat { get; set; }
        public string? Ysene {get; set; }
        public int? San { get; set; }
        public int? Atsan { get; set; }
        public string? Bellik { get; set; }
       
    }
    public class BlanketsResponse
    {  
        public int total { get; set; }
        public List<Model_Blankets> list { get; set; }
    }

    public class Model_Blankets 
     {
         string _guid = "";
         string _ykrarhat = "";
         string _ysene = "";      
         string _bellik = "";
         public string? guid { get { return _guid; } set { if (value == null) _guid = " "; else _guid = value; } }
         public string? ykrarhat { get { return _ykrarhat; } set { if (value == null) _ykrarhat = " "; else _ykrarhat = value; } }
         public string? ysene { get { return _ysene; } set { if (value == null) _ysene = " "; else _ysene = value; } }
         public int? san { get; set; }
         public int? atsan { get; set; }            
         public string? bellik { get { return _bellik; } set { if (value == null) _bellik = " "; else _bellik = value; } }
         public List<JHorse> horses { get; set; }
     }
     public class JHorse
     {   
         string _guid = "";
         string _lakamy = "";
         string _atasy = "";
         string _enesi = "";
         string _jynsy = "";
         string _renki = "";
         string _biomaterial = "";
         string _probnomer = "";
         string _eyesi = "";
         string _nyshanalar = "";
         string _bellik = "";
         public string guid { get { return _guid; } set { if (value == null) _guid = " "; else _guid = value; } }
         public string lakamy { get { return _lakamy; } set { if (value == null) _lakamy = " "; else _lakamy = value; } }
         public int? doglanyyl { get; set; }
         public string atasy { get { return _atasy; } set { if (value == null) _atasy = " "; else _atasy = value; } }
         public string enesi { get { return _enesi; } set { if (value == null) _enesi = " "; else _enesi = value; } }
         public string jynsy { get { return _jynsy; } set { if (value == null) _jynsy = " "; else _jynsy = value; } }
         public string renki { get { return _renki; } set { if (value == null) _renki = " "; else _renki = value; } }
         public string biomaterial { get { return _biomaterial; } set { if (value == null) _biomaterial = " "; else _biomaterial = value; } }
         public int? biosan { get; set; }
         public string probnomer { get { return _probnomer; } set { if (value == null) _probnomer = " "; else _probnomer = value; } }
         public string eyesi { get { return _eyesi; } set { if (value == null) _eyesi = " "; else _eyesi = value; } }
         public string nyshanalar { get { return _nyshanalar; } set { if (value == null) _nyshanalar = " "; else _nyshanalar = value; } }
         public string bellik { get { return _bellik; } set { if (value == null) _bellik = " "; else _bellik = value; } }

     }
    
}
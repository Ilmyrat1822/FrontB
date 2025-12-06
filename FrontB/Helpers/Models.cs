using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontB.Helpers
{
    public class LoginResponse
    {
        public Model_Login? data { get; set; }
    }
    public class Model_Login
    {
        string _token = "";
        string _refresh_token = "";
        public string token { get { return _token; } set { if (value == null) _token = " "; else _token = value; } }
        public string refresh_token { get { return _refresh_token; } set { if (value == null) _refresh_token = " "; else _refresh_token = value; } }
        public Admin? admin { get; set; }
    }
    public class Admin
    {
        string _id = "";
        string _name = "";
        string _surname = "";
        string _username = "";
        string _granted = "";

        public string? id { get { return _id; } set { if (value == null) _id = " "; else _id = value; } }
        public string? name { get { return _name; } set { if (value == null) _name = " "; else _name = value; } }
        public string? surname { get { return _surname; } set { if (value == null) _surname = " "; else _surname = value; } }
        public int? status { get; set; }
        public string? username { get { return _username; } set { if (value == null) _username = " "; else _username = value; } }
        public string? granted { get { return _granted; } set { if (value == null) _granted = " "; else _granted = value; } }
    }
    public class StatsResponse
    {
        public int stat1 { get; set; }
        public int stat2 { get; set; }
        public int stat3 { get; set; }
    }
    public class YearResponse
    {
        public List<int>? data { get; set; }
    }
    public class BlanketsResponse
    {
        public int total { get; set; }
        public List<Blanket>? list { get; set; }
    }    
    public class Blanket
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
        public List<JHorses>? horses { get; set; }
    }
    public class JHorses
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
        string _nyshanlar = "";
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
        public string nyshanlar { get { return _nyshanlar; } set { if (value == null) _nyshanlar = " "; else _nyshanlar = value; } }
        public string bellik { get { return _bellik; } set { if (value == null) _bellik = " "; else _bellik = value; } }
    }

    public class ColorsResponse
    {
        public List<Colors>? colors { get; set; }
    }
    public class Colors
    {
        string _renk = "";
        public int? id { get; set; }
        public string renk { get { return _renk; } set { if (value == null) _renk = " "; else _renk = value; } }
    }
    public class OwnersResponse
    {
        public List<Owners>? owners { get; set; }
    }
    public class Owners
    {        
        string _owner = "";
        public string owner { get { return _owner; } set { if (value == null) _owner = " "; else _owner = value; } }
    }
    public class JHorsesResponse
    {
        public int total { get; set; }
        public List<JHorses2>? list { get; set; }
    }
    public class JHorses2
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
        string _nyshanlar = "";
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
        public string nyshanlar { get { return _nyshanlar; } set { if (value == null) _nyshanlar = " "; else _nyshanlar = value; } }
        public string bellik { get { return _bellik; } set { if (value == null) _bellik = " "; else _bellik = value; } }
        public Blanket? blanket { get; set; }
    }
}
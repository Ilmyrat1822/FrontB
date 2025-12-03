using FrontB.Helpers;

namespace FrontB.Classes
{    
    public class BlanketsClass
    {
        public BlanketsClass(int? _counter, string? _guid, string? _ykrarhat, string? _ysene, int? _san, int? _atsan,string? _belllik,List<JHorse>? _horses)

        {
            this.Counter = _counter;
            this.Guid = _guid;
            this.Ykrarhat = _ykrarhat;
            this.Ysene = _ysene;
            this.San = _san;
            this.Atsan = _atsan;
            this.Horses = _horses;
        }            

        public int? Counter { get; set; }
        public string? Guid { get; set; }
        public string? Ykrarhat { get; set; }
        public string? Ysene {get; set; }
        public int? San { get; set; }
        public int? Atsan { get; set; }
        public string? Bellik { get; set; }
        public List<JHorse>? Horses { get; set; }
    }
    
}
using FrontB.Helpers;

namespace FrontB.Classes
{    
    public class BlanketsClass
    {
        public BlanketsClass(uint? _id, int? _tb, string? _ykrarhat, string? _sene, int? _atsan, int? _atcount,string? _belllik,List<JHorses>? _horses)

        {
            this.Id = _id;
            this.TB = _tb;
            this.Ykrarhat = _ykrarhat;
            this.Sene = _sene;
            this.Atsan = _atsan;
            this.Atcount = _atcount;
            this.Horses = _horses;
        }            

        public uint? Id { get; set; }
        public int? TB { get; set; }
        public string? Ykrarhat { get; set; }
        public string? Sene {get; set; }
        public int? Atsan { get; set; }
        public int? Atcount { get; set; }
        public string? Bellik { get; set; }
        public List<JHorses>? Horses { get; set; }
    }
    
}
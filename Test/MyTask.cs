using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class MyTask
    {
        // bu test uchun qilindi
        // bu ham


        public int Id { get; set; }
        public string Message { get; set; }
        public int? TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public int PupulId { get; set; }
        public Pupil Pupil { get; set; }
        public DateTime CheckedTime { get; set; }
        public int Ball { get; set; }
        public bool IsChecked { get; set; } = false;

    }
}

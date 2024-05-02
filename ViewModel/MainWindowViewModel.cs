using Model;
using Model.DataAccess;

namespace ViewModel
{
    public class MainWindowViewModel
    {
        private Context _context = new();

        public List<Student> Students { get; set; }
        
        public MainWindowViewModel() 
        {
            Students = [.. _context.Students];
        }
    }
}

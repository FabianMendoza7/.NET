using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace LeerData
{
    class Program
    {
        static void Main(string[] args)
        {
            using(var db = new AppVentaCursosContext()){
                // var cursos = db.Curso.AsNoTracking(); // Arreglo de objeto IQueryable

                // foreach(var curso in cursos){
                //     Console.WriteLine(curso.Titulo);
                // }

                // var cursos = db.Curso.Include(p => p.PrecioPromocion).AsNoTracking();
                // foreach(var curso in cursos){
                //     Console.WriteLine(curso.Titulo);
                // }

                // var cursos = db.Curso.Include(c => c.InstructorLink).ThenInclude(ci => ci.Instructor);
                // foreach(var curso in cursos){
                //     Console.WriteLine(curso.Titulo);
                //     foreach(var insLink in curso.InstructorLink){
                //         Console.WriteLine("***" + insLink.Instructor.Nombre);
                //     }
                // }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WinfocusLearningApp.DataEntity;

namespace WinfocusLearningApp.Models
{
    public class MaterialModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Nullable<int> ACID { get; set; }
        public Nullable<int> SyllabusID { get; set; }
        public Nullable<int> GradeID { get; set; }
        public Nullable<int> StreamID { get; set; }
        public Nullable<int> CourseID { get; set; }
        public int SubjectId { get; set; }
        public int CreatedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int IsDeleted { get; set; }
        public System.DateTime DeletedDate { get; set; }
        public int ModifiedBy { get; set; }
        public System.DateTime ModifiedTime { get; set; }

        public List<TblMaterial> tblMaterials { get; set; }
    }
}
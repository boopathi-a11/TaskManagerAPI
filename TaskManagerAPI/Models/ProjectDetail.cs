
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace TaskManagerAPI.Models
{

using System;
    using System.Collections.Generic;
    
public partial class ProjectDetail
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public ProjectDetail()
    {

        this.TaskDetails = new HashSet<TaskDetail>();

    }


    public int Project_ID { get; set; }

    public string Project_Name { get; set; }

    public System.DateTime Start_date { get; set; }

    public System.DateTime End_date { get; set; }

    public int Project_Priority { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<TaskDetail> TaskDetails { get; set; }

}

}

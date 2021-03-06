﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainClass.Models
{
    public class Comments
    {
        public Comments()
        {
            //this.Tbl_Cat_News = new HashSet<Tbl_Cat_News>();
            //this.Tbl_Comments = new HashSet<Tbl_Comments>();
        }
        public int ID { get; set; }
        public int NewsID { get; set; }
        public int ParentID { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Text { get; set; }
        public string IP { get; set; }
        public System.DateTime? Date { get; set; }
        public bool Confirm { get; set; }
        public bool IsDeleted { get; set; }
        public bool Read { get; set; }
        public int Like { get; set; }
        public int Dislike { get; set; }

        public Nullable<int> CreatedBy { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        public virtual News News { get; set; }
        
        //public partial class Tbl_Comments
        //{
        //    public int ID { get; set; }
        //    public int NewsID { get; set; }
        //    public int ParentID { get; set; }
        //    public string Title { get; set; }
        //    public string Name { get; set; }
        //    public string Email { get; set; }
        //    public string Text { get; set; }
        //    public string IP { get; set; }
        //    public System.DateTime Date { get; set; }
        //    public bool Confirm { get; set; }
        //    public bool Read { get; set; }
        //    public int Like { get; set; }
        //    public int Dislike { get; set; }

        //    public virtual Tbl_News Tbl_News { get; set; }
        //}


        //public Tbl_Catgoris()
        //{
        //    this.Tbl_Cat_News = new HashSet<Tbl_Cat_News>();
        //    this.Tbl_Catgoris1 = new HashSet<Tbl_Catgoris>();
        //}

        //public int ID { get; set; }
        //public string Title { get; set; }
        //public int ParentID { get; set; }
        //public string Image { get; set; }

        //public virtual ICollection<Tbl_Cat_News> Tbl_Cat_News { get; set; }
        //public virtual ICollection<Tbl_Catgoris> Tbl_Catgoris1 { get; set; }
        //public virtual Tbl_Catgoris Tbl_Catgoris2 { get; set; }


        //public partial class Tbl_ContactUs
        //{
        //    public int ID { get; set; }
        //    public string Name { get; set; }
        //    public string Email { get; set; }
        //    public string Text { get; set; }
        //}

        //public partial class Tbl_Ads
        //{
        //    public int ID { get; set; }
        //    public string Title { get; set; }
        //    public string Description { get; set; }
        //    public string Image { get; set; }
        //    public string Link { get; set; }
        //    public System.DateTime Date { get; set; }
        //    public System.DateTime DateStart { get; set; }
        //    public System.DateTime DateEnd { get; set; }
        //    public bool Enable { get; set; }
        //}
    }
}


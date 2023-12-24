using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DRN_WEB_ERP.GlobalClass
{
    public class clsMpo
    {
        public clsMpo()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        private int seq;
        private int lno;
        private string reqtype;
        private string refno;
        private string icode;
        private string idet;
        private string pcode;
        private string pdet;
        private string uom;
        private decimal qnty;
        private decimal rate;
        private decimal totval;
        private string specification;
        private string brand;
        private string origin;
        private string packing;
        private string empdet;
        private string partydet;

        public int Seq
        {
            get { return seq; }
            set { seq = value; }
        }
        public int Lno
        {
            get { return lno; }
            set { lno = value; }
        }
        public String ReqType
        {
            get { return reqtype; }
            set { reqtype = value; }
        }
        public String RefNo
        {
            get { return refno; }
            set { refno = value; }
        }
        public String Icode
        {
            get { return icode; }
            set { icode = value; }
        }
        public String Idet
        {
            get { return idet; }
            set { idet = value; }
        }
        public String Uom
        {
            get { return uom; }
            set { uom = value; }
        }
        public String Pcode
        {
            get { return pcode; }
            set { pcode = value; }
        }
        public String Pdet
        {
            get { return pdet; }
            set { pdet = value; }
        }
        public Decimal Qnty
        {
            get { return qnty; }
            set { qnty = value; }
        }
        public Decimal Rate
        {
            get { return rate; }
            set { rate = value; }
        }
        public Decimal Totval
        {
            get { return totval; }
            set { totval = value; }
        }
        public String Specification
        {
            get { return specification; }
            set { specification = value; }
        }
        public String Brand
        {
            get { return brand; }
            set { brand = value; }
        }
        public String Origin
        {
            get { return origin; }
            set { origin = value; }
        }
        public String Packing
        {
            get { return packing; }
            set { packing = value; }
        }
        public String Partydet
        {
            get { return partydet; }
            set { partydet = value; }
        }
        public String Empdet
        {
            get { return empdet; }
            set { empdet = value; }
        }
    }
}
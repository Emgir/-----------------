namespace Несчастный_случай {
    
    
    public partial class DataSet1 {

        public void LoadXml()
        {
            this.Clear();
            this.ReadXml(@"XML/danger.xml");
        }
        public void SaveXml()
        {
            this.WriteXml(@"XML/danger.xml");
        }
        }
        

    
}

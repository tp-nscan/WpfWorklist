using System;

namespace Lab.Model.Containers
{
    public class VialPosition
    {
        public VialPosition(string label)
        {
            Label = label;
        }

        public VialPosition(int towerIndex, int plateIndex, int wellIndex)
        {
            _label = string.Format("CStk{0}-0{1}:{2}", towerIndex, plateIndex, wellIndex);
        }

        private string _label;
        public string Label
        {
            get { return _label; }
            private set
            {
                _label = value;
                try
                {
                    var pcs1 = value.Split(":".ToCharArray());
                    WellIndex = int.Parse(pcs1[1]);
                    var pcs2 = pcs1[0].Split("-".ToCharArray());
                    PlateIndex = int.Parse(pcs2[1]);
                    TowerIndex = int.Parse(pcs2[0].Replace("CStk",string.Empty));
                    IsValid = true;
                }
                catch (Exception)
                {
                    IsValid = false;
                }
            }
        }

        public int WellIndex { get; private set; }

        public bool IsValid { get; private set; }

        public int PlateIndex { get; private set; }

        public int TowerIndex { get; private set; }
    }

}

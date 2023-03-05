using GOAP.Base;

namespace GOAP.Agents
{
    public class Nurse : GAgent
    {
        // Start is called before the first frame update
        void Start()
        {
            base.Start();
            SubGoal s1 = new SubGoal("treatPatient", 1, true);
            goals.Add(s1, 3);
        }
    }
}

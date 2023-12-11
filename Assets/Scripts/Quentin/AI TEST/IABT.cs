using BehaviorTree;

public class IABT : Tree
{
    public UnityEngine.Transform[] waypoints;

    public static float speed = 2f;
    protected override Node SetupTree()
    {
        Node root = new Patrol(transform, waypoints);
        throw new System.NotImplementedException(); //false
    }

}

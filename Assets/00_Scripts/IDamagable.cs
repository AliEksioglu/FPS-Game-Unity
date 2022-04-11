
public struct AttackDefiniton 
{
    public IAttackable atacker;
    public float damagePoint;
   
}

public interface IDamagable 
{
    public void GetDamagable(AttackDefiniton attackDefiniton);

}

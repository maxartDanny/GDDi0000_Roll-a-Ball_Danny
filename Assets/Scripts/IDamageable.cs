
using UnityEngine;
/// <summary>
///
/// </summary>
public interface IDamageable {

    IDDamage MyDamageID();

    void DamageRecieve(Transform other, IDDamage damageType, Vector3 sourcePos, Vector3 velocity);
}

using UnityEngine;
/// <summary>
///
/// </summary>
public interface IDamageable {

    IDDamage GetDamageID();

    void DamageRecieve(IDDamage damageType, Vector3 sourcePos, Vector3 velocity);
}
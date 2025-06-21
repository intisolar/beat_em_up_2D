using NUnit.Framework;
using UnityEngine;

public class CombatTests
{
    [SetUp]
    public void Setup()
    {

    }

    [TearDown]
    public void Teardown()
    {
        // Destruyo inmediatamente para no dejar basura en memoria
        // Object.DestroyImmediate(object);
    }

    [Test]
    public void Test_ResolveCombat_EnemyAttackSucceeds()
    {

    }
}

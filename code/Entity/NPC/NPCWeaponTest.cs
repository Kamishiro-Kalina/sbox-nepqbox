using Sandbox;
using System.Linq;

[Library( "npc_weapon_test", Title = "Weapon Test", Spawnable = true )]
public partial class NPCWeaponTest : NPC
{
	public override bool UseWeapon => true;

	public override void Spawn()
	{
		base.Spawn();

		Inventory.Add( new Pistol(), true );
	}

	public override void OnTick()
	{
		if ( ActiveChild is Weapon wep && wep.CanNPCPrimaryAttack() )
		{
			wep.NPCAttackPrimary();
		}
	}
}
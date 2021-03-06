using Sandbox;
using System;

[Spawnable]
[Library( "weapon_boneknife", Title = "Bone Knife" )]
[EditorModel("weapons/rust_boneknife/rust_boneknife.vmdl")]
partial class BoneKnife : WeaponMelee
{
	public override string ViewModelPath => "weapons/rust_boneknife/v_rust_boneknife.vmdl";
	public override string WorldModelPath => "weapons/rust_boneknife/rust_boneknife.vmdl";

	public override int Bucket => 0;
	public override float PrimarySpeed => 1f;
	public override float SecondarySpeed => 2f;
	public override float PrimaryDamage => 35f;
	public override float PrimaryForce => 100f * 1.5f;
	public override float SecondaryDamage => 35f * 3f;
	public override float SecondaryForce => 100f * 1.5f;
	public override float PrimaryMeleeDistance => 80f;
	public override CType Crosshair => CType.None;
	public override string Icon => "ui/weapons/weapon_boneknife.png";
	public override string PrimaryAnimationHit => "fire";
	public override string PrimaryAnimationMiss => "fire";
	public override string SecondaryAnimationHit => "fire";
	public override string SecondaryAnimationMiss => "fire";
	public override string PrimaryAttackSound => "rust_boneknife.attack";
	public override string HitWorldSound => "rust_boneknife.attack";
	public override string MissSound => "rust_boneknife.attack";
	public override bool CanUseSecondary => true;

	public override void SimulateAnimator(PawnAnimator anim)
	{
		anim.SetAnimParameter("holdtype", 4); // TODO this is shit
		anim.SetAnimParameter("holdtype_attack", 2.0f);
		anim.SetAnimParameter("holdtype_handedness", 1);
		anim.SetAnimParameter("holdtype_pose", 0f);
		anim.SetAnimParameter("aim_body_weight", 1.0f);
	}
}

using Sandbox;

[Spawnable]
[Library( "weapon_ak47", Title = "AK47" )]
[EditorModel( "weapons/swb/css/ak47/css_w_rif_ak47.vmdl" )]
partial class AK47 : Weapon
{
	public override string ViewModelPath => "weapons/swb/css/ak47/css_v_rif_ak47.vmdl";
	public override string WorldModelPath => "weapons/swb/css/ak47/css_w_rif_ak47.vmdl";

	public override int ClipSize => 30;
	public override int Bucket => 2;
	public override float PrimaryRate => 10f;
	public override float ReloadTime => 2.17f;
	public override CType Crosshair => CType.Rifle;
	public override string Icon => "ui/weapons/weapon_ak47.png";
	public override string ShootSound => "css_ak47.fire";
	public override float Spread => 0.1f;
	public override float Force => 3f;
	public override float Damage => 15f;
	public override float BulletSize => 4f;
	public override float FOV => 75;
	public override ScreenShake ScreenShake => new ScreenShake
	{
		Length = 0.5f,
		Delay = 4.0f,
		Size = 0.5f,
		Rotation = 0.5f
	};

	public override void SimulateAnimator( PawnAnimator anim )
	{
		anim.SetAnimParameter( "holdtype", 2 ); // TODO this is shit
		anim.SetAnimParameter( "aim_body_weight", 1.0f );
	}

	public override void NPCAnimator( NPC npc )
	{
		npc.SetAnimParameter( "holdtype", 2 ); // TODO this is shit
		npc.SetAnimParameter( "aim_body_weight", 1.0f );
	}
}

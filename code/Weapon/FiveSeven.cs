using Sandbox;

[Spawnable]
[Library( "weapon_fiveseven", Title = "Five-Seven" )]
[EditorModel( "weapons/css_fiveseven/w_css_pist_fiveseven.vmdl" )]
partial class FiveSeven : Weapon
{
	public override string ViewModelPath => "weapons/css_fiveseven/v_css_pist_fiveseven.vmdl";
	public override string WorldModelPath => "weapons/css_fiveseven/w_css_pist_fiveseven.vmdl";

	public override int ClipSize => 20;
	public override int Bucket => 1;
	public override int AmmoMultiplier => 5;
	public override bool Automatic => false;
	public override float PrimaryRate => 10f;
	public override float ReloadTime => 3.2f;
	public override CType Crosshair => CType.Pistol;
	public override string Icon => "ui/weapons/weapon_fiveseven.png";
	public override string ShootSound => "css_fiveseven.fire";
	public override float Spread => 0.15f;
	public override float Force => 5f;
	public override float Damage => 30f;
	public override float BulletSize => 3f;
	public override float FOV => 75;
	public override ScreenShake ScreenShake => new ScreenShake
	{
		Length = 0.5f,
		Delay = 4.0f,
		Size = 1.0f,
		Rotation = 0.5f
	};

	public override void SimulateAnimator( PawnAnimator anim )
	{
		anim.SetAnimParameter( "holdtype", 1 ); // TODO this is shit
		anim.SetAnimParameter( "holdtype_handedness", 0 );
		anim.SetAnimParameter( "aim_body_weight", 1.0f );
	}
}

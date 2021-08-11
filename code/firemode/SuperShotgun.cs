namespace Sandbox.CWEP
{
	[Library( "cwep_supershotgun", Title = "Super Shot Gun", Description = "Super powerful shotgun" )]
	public partial class SuperShotGun : CWEPFireMode
	{
		public override string PrimarySoundName => "rust_pumpshotgun.shootdouble";
		public override CType Crosshair => CType.ShotGun;

		public override void AttackPrimary()
		{
			Parent.ShootBullets( 20, 0.4f, 20.0f, 8.0f, 3.0f );

			Owner.ApplyAbsoluteImpulse( Owner.EyeRot.Backward * 1000.0f );
		}
	}
}
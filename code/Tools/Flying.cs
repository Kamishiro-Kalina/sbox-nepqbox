﻿using Sandbox;


[Library("weapon_flying", Title = "Flying", Spawnable = true )]
[Hammer.EditorModel( "weapons/rust_pistol/rust_pistol.vmdl" )]
partial class Flying : Carriable
{ 
	public override string ViewModelPath => "weapons/rust_pistol/v_rust_pistol.vmdl";
	public override int Bucket => 8;
	public override string Icon => "ui/weapons/weapon_pistol.png";
	private PawnController LastController;
	private bool isFlying;

	public override void Spawn()
	{
		base.Spawn();

		SetModel( "weapons/rust_pistol/rust_pistol.vmdl" );
	}

	private void Activate()
    {
		if (Owner is not Player owner) return;

		LastController = owner.Controller;
		owner.Controller = new SandboxFlyingController();

		isFlying = true;
	}

	private void Deactivate()
    {
		if ( Owner is not Player owner || LastController == null ) return;

		owner.Controller = LastController;

		isFlying = false;
	}

	public override void ActiveEnd(Entity ent, bool dropped)
	{
		base.ActiveEnd(ent, dropped);

		Deactivate();
	}

	public override void Simulate( Client cl )
	{
		base.Simulate( cl );

		var ply = Owner as Player;
		if ( ply == null ) return;

		if ( isFlying )
		{
			var tr = Trace.Ray( Position, Position + Vector3.Down * 100 )
				.Radius( 1 )
				.Ignore( this )
				.Run();

			if ( tr.Hit )
				Deactivate();
		}
		else
		{
			if ( ply.Controller.GroundEntity == null )
				Activate();
		}
	}

	protected override void OnDestroy()
	{
		Deactivate();

		base.OnDestroy();
	}

	public override void OnCarryDrop(Entity dropper)
	{
		Deactivate();

		base.OnCarryDrop(dropper);
	}
}
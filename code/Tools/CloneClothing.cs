using System.Collections.Generic;

namespace Sandbox.Tools
{
	[Library( "tool_cloneclothing", Title = "Clone Clothing", Description = "Clone your own clothing on any citizen model ( Rag dolls can also )", Group = "fun" )]
	public partial class CloneClothing : BaseTool
	{
		List<AnimEntity> ClothingModels = new();
		Dictionary<Entity, List<string>> OldClothing = new();

		public override void Simulate()
		{
			if ( !Host.IsServer )
				return;

			var startPos = Owner.EyePos;
			var dir = Owner.EyeRot.Forward;

			var tr = Trace.Ray( startPos, startPos + dir * MaxTraceDistance )
				.Ignore( Owner )
				.Run();

			if ( !tr.Hit || !tr.Entity.IsValid() || !(tr.Entity is ModelEntity e) || e.GetModelName() != "models/citizen/citizen.vmdl" )
				return;

			if ( Input.Pressed( InputButton.Attack1 ) )
			{
				CreateHitEffects( tr.EndPos );
				DeleteAllDress( e );

				if ( e is AnimEntity ent )
				{
					var Clothing = new Clothing.Container();

					Clothing.LoadFromClient( Owner.Client );
					Clothing.DressEntity( ent );
				}
				else
				{
					var Clothing = new Clothing.Container();

					Clothing.LoadFromClient( Owner.Client );
					Clothing.DressEntity( e, ClothingModels );
				}
			}

			if ( Input.Pressed( InputButton.Attack2 ) )
			{
				if ( OldClothing.ContainsKey( e ) )
				{
					CreateHitEffects( tr.EndPos );
					DeleteAllDress( e );

					foreach ( var model in OldClothing[e] )
					{
						new ModelEntity( model, e ).Tags.Add( "clothes" );
					}
				}
			}
		}

		private void DeleteAllDress( ModelEntity ent )
		{
			List<string> list = new();

			while ( ent.Children.Count > 0 )
			{
				for ( var i = 0; i < ent.Children.Count; i++ )
				{
					var child = ent.Children[i];

					if ( !child.Tags.Has( "clothes" ) ) continue;
					if ( child is not ModelEntity e ) continue;

					list.Add( e.GetModelName() );

					child.Delete();
				}
			}

			ent.SetBodyGroup( "head", 0 );
			ent.SetBodyGroup( "Chest", 0 );
			ent.SetBodyGroup( "Legs", 0 );
			ent.SetBodyGroup( "Hands", 0 );
			ent.SetBodyGroup( "Feet", 0 );

			if ( !OldClothing.ContainsKey( ent ) )
				OldClothing.Add( ent, list );
		}
	}

	public static class ClothingContainerEX
	{
		public static void ClearModelEntities( this Clothing.Container Clothing, List<AnimEntity> list )
		{
			foreach ( var model in list )
			{
				model.Delete();
			}
			list.Clear();
		}

		public static void DressEntity( this Clothing.Container Clothing, ModelEntity model, List<AnimEntity> list )
		{
			//
			// Start with defaults
			//
			model.SetMaterialGroup( "default" );

			//
			// Remove old models
			//
			Clothing.ClearEntities();
			Clothing.ClearModelEntities( list );

			//
			// Create clothes models
			//
			foreach ( var c in Clothing.Clothing )
			{
				if ( c.Model == "models/citizen/citizen.vmdl" )
				{
					model.SetMaterialGroup( c.MaterialGroup );
					continue;
				}

				var anim = new AnimEntity( c.Model, model );

				anim.Tags.Add( "clothes" );

				if ( !string.IsNullOrEmpty( c.MaterialGroup ) )
					anim.SetMaterialGroup( c.MaterialGroup );

				list.Add( anim );
			}

			//
			// Set body groups
			//
			foreach ( var group in Clothing.GetBodyGroups() )
			{
				model.SetBodyGroup( group.name, group.value );
			}
		}
	}
}

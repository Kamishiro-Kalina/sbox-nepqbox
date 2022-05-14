﻿using Sandbox;
using Sandbox.UI;
using Sandbox.UI.Construct;
using Sandbox.UI.Tests;
using System.Linq;

[Library]
public partial class WeaponList : Panel
{
	VirtualScrollPanel Canvas;

	public WeaponList()
	{
		AddClass( "spawnpage" );
		AddChild( out Canvas, "canvas" );

		Canvas.Layout.AutoColumns = true;
		Canvas.Layout.ItemHeight = 100;
		Canvas.Layout.ItemWidth = 100;
		Canvas.OnCreateCell = ( cell, data ) =>
		{
			var entry = (LibraryAttribute)data;
			var path = $"/entity/{entry.Name}.png";

			if ( !FileSystem.Mounted.FileExists( path ) )
				path = $"/ui/weapons/{entry.Name}.png";

			var btn = cell.Add.Button( entry.Title );
			btn.AddClass( "icon" );
			btn.AddEventListener( "onclick", () => ConsoleSystem.Run( "spawn_entity", entry.Name ) );
			btn.Style.BackgroundImage = Texture.Load( FileSystem.Mounted, path, false );
		};

		LoadAllItem( false );
	}

	private void LoadAllItem( bool isreload )
	{
		if ( isreload )
			Canvas.Data.Clear();

		var ents = Library.GetAllAttributes<Weapon>().Where( x => x.Spawnable ).OrderBy( x => x.Title ).ToArray();

		foreach ( var entry in ents )
		{
			Canvas.AddItem( entry );
		}
	}

	public override void OnHotloaded()
	{
		base.OnHotloaded();

		LoadAllItem( true );
	}
}

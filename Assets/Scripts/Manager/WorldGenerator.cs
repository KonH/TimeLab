using TimeLab.Command;
using TimeLab.Component;
using TimeLab.Shared;
using UnityEngine;

namespace TimeLab.Manager {
	public sealed class WorldGenerator {
		readonly CommandStorage _storage;
		readonly IdGenerator    _idGenerator;

		public WorldGenerator(CommandStorage storage, IdGenerator idGenerator) {
			_storage     = storage;
			_idGenerator = idGenerator;
		}

		public void Generate() {
			var worldCommands    = _storage.GetWorldCommands();
			var firstLocationId  = _idGenerator.GetNextId();
			var secondLocationId = _idGenerator.GetNextId();
			worldCommands.Insert(0, new AddLocationCommand(firstLocationId, new Rect2DInt(0, 1, 10, 6)));
			worldCommands.Insert(1, new AddLocationCommand(secondLocationId, new Rect2DInt(0, -8, 10, 6)));
			var firstLocationCommands = _storage.GetLocationCommands(firstLocationId);
			var firstDoorId           = _idGenerator.GetNextId();
			firstLocationCommands.Insert(0, new AddEntityCommand(
				firstDoorId,
				new Vector2Int(0, -3)));
			firstLocationCommands.Insert(1, new AddComponentCommand(
				firstDoorId, new PortalComponent(secondLocationId, new Vector2Int(0, 3))));
			firstLocationCommands.Insert(2, new AddComponentCommand(
				firstDoorId, new RenderComponent("Door")));
			var secondLocationCommands = _storage.GetLocationCommands(secondLocationId);
			var secondDoorId           = _idGenerator.GetNextId();
			secondLocationCommands.Insert(0, new AddEntityCommand(
				secondDoorId,
				new Vector2Int(0, 3)));
			secondLocationCommands.Insert(1, new AddComponentCommand(
				secondDoorId, new PortalComponent(firstLocationId, new Vector2Int(0, -3))));
			secondLocationCommands.Insert(2, new AddComponentCommand(
				secondDoorId, new RenderComponent("Door")));
		}
	}
}
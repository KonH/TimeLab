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
			var time             = double.MinValue;
			var worldCommands    = _storage.GetWorldCommands();
			var firstLocationId  = _idGenerator.GetNextId();
			var secondLocationId = _idGenerator.GetNextId();
			worldCommands.Enqueue(time, new AddLocationCommand(firstLocationId, new Rect2DInt(0, 1, 10, 6)));
			worldCommands.Enqueue(time, new AddLocationCommand(secondLocationId, new Rect2DInt(0, -8, 10, 6)));
			var firstLocationCommands = _storage.GetLocationCommands(firstLocationId);
			var firstDoorId           = _idGenerator.GetNextId();
			firstLocationCommands.Enqueue(time, new AddEntityCommand(
				firstDoorId,
				new Vector2Int(0, -3)));
			firstLocationCommands.Enqueue(time, new AddEntityComponentCommand(
				firstDoorId, new PortalComponent(secondLocationId, new Vector2Int(0, 3))));
			firstLocationCommands.Enqueue(time, new AddEntityComponentCommand(
				firstDoorId, new RenderComponent("Door")));
			var secondLocationCommands = _storage.GetLocationCommands(secondLocationId);
			var secondDoorId           = _idGenerator.GetNextId();
			secondLocationCommands.Enqueue(time, new AddEntityCommand(
				secondDoorId,
				new Vector2Int(0, 3)));
			secondLocationCommands.Enqueue(time, new AddEntityComponentCommand(
				secondDoorId, new PortalComponent(firstLocationId, new Vector2Int(0, -3))));
			secondLocationCommands.Enqueue(time, new AddEntityComponentCommand(
				secondDoorId, new RenderComponent("Door")));
		}
	}
}
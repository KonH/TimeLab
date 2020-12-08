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
			firstLocationCommands.Insert(0, new AddEntityCommand(
				_idGenerator.GetNextId(),
				new Vector2Int(0, -3),
				new PortalComponent(secondLocationId, new Vector2Int(0, 3)),
				new RenderComponent("Door")));
			firstLocationCommands.Insert(1, new AddEntityCommand(
				_idGenerator.GetNextId(),
				new Vector2Int(5, 3),
				new BotComponent(),
				new RenderComponent("Bot")));
			var secondLocationCommands = _storage.GetLocationCommands(secondLocationId);
			secondLocationCommands.Insert(0, new AddEntityCommand(
				_idGenerator.GetNextId(),
				new Vector2Int(0, 3),
				new PortalComponent(firstLocationId, new Vector2Int(0, -3)),
				new RenderComponent("Door")));
			secondLocationCommands.Insert(1, new AddEntityCommand(
				_idGenerator.GetNextId(),
				new Vector2Int(-5, -3),
				new BotComponent(),
				new RenderComponent("Bot")));
		}
	}
}
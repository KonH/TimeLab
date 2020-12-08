using TimeLab.Command;
using TimeLab.Component;
using TimeLab.Shared;
using UnityEngine;

namespace TimeLab.Systems {
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
			worldCommands.Enqueue(0, new AddLocationCommand(firstLocationId, new Rect2DInt(0, 1, 10, 6)));
			worldCommands.Enqueue(0, new AddLocationCommand(secondLocationId, new Rect2DInt(0, -8, 10, 6)));
			var firstLocationCommands = _storage.GetLocationCommands(firstLocationId);
			firstLocationCommands.Enqueue(0, new AddEntityCommand(
				_idGenerator.GetNextId(),
				Vector2Int.zero,
				new PlayerComponent(),
				new RenderComponent("Player")));
			firstLocationCommands.Enqueue(0, new AddEntityCommand(
				_idGenerator.GetNextId(),
				new Vector2Int(5, 3),
				new BotComponent(),
				new RenderComponent("Bot")));
			firstLocationCommands.Enqueue(0, new AddEntityCommand(
				_idGenerator.GetNextId(),
				new Vector2Int(0, -3),
				new PortalComponent(secondLocationId, new Vector2Int(0, 3)),
				new RenderComponent("Door")));
			var secondLocationCommands = _storage.GetLocationCommands(secondLocationId);
			secondLocationCommands.Enqueue(0, new AddEntityCommand(
				_idGenerator.GetNextId(),
				new Vector2Int(0, 3),
				new PortalComponent(firstLocationId, new Vector2Int(0, -3)),
				new RenderComponent("Door")));
			secondLocationCommands.Enqueue(0, new AddEntityCommand(
				_idGenerator.GetNextId(),
				new Vector2Int(-5, -3),
				new BotComponent(),
				new RenderComponent("Bot")));
		}
	}
}
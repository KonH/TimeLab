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
			var worldCommands = _storage.GetWorldCommands();
			worldCommands.Enqueue(0, new AddLocationCommand(_idGenerator.GetNextId(), new Rect2DInt(0, 0, 10, 6)));
			var locationCommands = _storage.GetLocationCommands(1);
			locationCommands.Enqueue(0, new AddEntityCommand(
				_idGenerator.GetNextId(),
				Vector2Int.zero,
				new PlayerComponent(),
				new RenderComponent("Player")));
			locationCommands.Enqueue(0, new AddEntityCommand(
				_idGenerator.GetNextId(),
				new Vector2Int(0, -3),
				new RenderComponent("Door")));
		}
	}
}
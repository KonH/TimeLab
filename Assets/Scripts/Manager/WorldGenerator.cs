using TimeLab.Command;
using TimeLab.Component;
using TimeLab.Shared;
using UnityEngine;

namespace TimeLab.Manager {
	public sealed class WorldGenerator {
		readonly IdGenerator _idGenerator;

		public WorldGenerator(IdGenerator idGenerator) {
			_idGenerator = idGenerator;
		}

		public CommandStorage Generate() {
			var time             = double.MinValue;
			var storage          = new CommandStorage();
			var playerLocationId  = _idGenerator.GetNextId();
			var streetLocationId = _idGenerator.GetNextId();
			AddPlayerLocation(time, playerLocationId, storage);
			AddStreetLocation(time, streetLocationId, storage);
			ConnectLocations(
				time, storage, playerLocationId, streetLocationId, new Vector2Int(0, -3), new Vector2Int(0, 3));
			return storage;
		}

		void AddPlayerLocation(double time, ulong locationId, CommandStorage storage) {
			var worldCommands   = storage.GetWorldCommands();
			worldCommands.Enqueue(time, new AddLocationCommand(locationId, new Rect2DInt(0, 1, 10, 6)));

			var locationCommands = storage.GetLocationCommands(locationId);
			AddFridge(time, locationCommands);
		}

		void AddStreetLocation(double time, ulong streetId, CommandStorage storage) {
			var worldCommands = storage.GetWorldCommands();
			worldCommands.Enqueue(time, new AddLocationCommand(streetId, new Rect2DInt(0, -8, 10, 6)));
			worldCommands.Enqueue(time, new AddLocationComponentCommand(streetId, new RefillArea("Stress", 1)));
		}

		void ConnectLocations(
			double time, CommandStorage storage,
			ulong fromLocationId, ulong toLocationId, Vector2Int fromPosition, Vector2Int toPosition) {
			AddDoor(time, storage.GetLocationCommands(fromLocationId), fromPosition, toLocationId, toPosition);
			AddDoor(time, storage.GetLocationCommands(toLocationId), toPosition, fromLocationId, fromPosition);
		}

		void AddDoor(
			double time, PermanentCommandQueue<ILocationCommand> locationCommands,
			Vector2Int localPosition, ulong targetLocationId, Vector2Int targetPosition) {
			var doorId = _idGenerator.GetNextId();
			locationCommands.Enqueue(time, new AddEntityCommand(
				doorId, localPosition));
			locationCommands.Enqueue(time, new AddEntityComponentCommand(
				doorId, new PortalComponent(targetLocationId, targetPosition)));
			locationCommands.Enqueue(time, new AddEntityComponentCommand(
				doorId, new RenderComponent("Door")));
		}

		void AddFridge(double time, PermanentCommandQueue<ILocationCommand> locationCommands) {
			var fridgeId = _idGenerator.GetNextId();
			locationCommands.Enqueue(time, new AddEntityCommand(
				fridgeId, new Vector2Int(-3, 3)));
			locationCommands.Enqueue(time, new AddEntityComponentCommand(
				fridgeId, new RefillSource("Hunger", 10, 5)));
			locationCommands.Enqueue(time, new AddEntityComponentCommand(
				fridgeId, new RenderComponent("Fridge")));
		}
	}
}
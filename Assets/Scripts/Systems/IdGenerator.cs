namespace TimeLab.Systems {
	/// <summary>
	/// Creates incremental IDs for all state entries
	/// </summary>
	public sealed class IdGenerator {
		ulong _lastId;

		public ulong GetNextId() {
			_lastId++;
			return _lastId;
		}
	}
}
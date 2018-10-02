using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sink {
	public interface RoomEnterEvent {
		void Trigger(Player p);
	}
}
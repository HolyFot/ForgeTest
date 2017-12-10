using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0.15,0.15,0,0,0,0,0]")]
	public partial class PlayerNetworkObject : NetworkObject
	{
		public const int IDENTITY = 2;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		private Vector3 _position;
		public event FieldEvent<Vector3> positionChanged;
		public InterpolateVector3 positionInterpolation = new InterpolateVector3() { LerpT = 0.15f, Enabled = true };
		public Vector3 position
		{
			get { return _position; }
			set
			{
				// Don't do anything if the value is the same
				if (_position == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_position = value;
				hasDirtyFields = true;
			}
		}

		public void SetpositionDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_position(ulong timestep)
		{
			if (positionChanged != null) positionChanged(_position, timestep);
			if (fieldAltered != null) fieldAltered("position", _position, timestep);
		}
		private Quaternion _rotation;
		public event FieldEvent<Quaternion> rotationChanged;
		public InterpolateQuaternion rotationInterpolation = new InterpolateQuaternion() { LerpT = 0.15f, Enabled = true };
		public Quaternion rotation
		{
			get { return _rotation; }
			set
			{
				// Don't do anything if the value is the same
				if (_rotation == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_rotation = value;
				hasDirtyFields = true;
			}
		}

		public void SetrotationDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_rotation(ulong timestep)
		{
			if (rotationChanged != null) rotationChanged(_rotation, timestep);
			if (fieldAltered != null) fieldAltered("rotation", _rotation, timestep);
		}
		private int _hp;
		public event FieldEvent<int> hpChanged;
		public Interpolated<int> hpInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int hp
		{
			get { return _hp; }
			set
			{
				// Don't do anything if the value is the same
				if (_hp == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x4;
				_hp = value;
				hasDirtyFields = true;
			}
		}

		public void SethpDirty()
		{
			_dirtyFields[0] |= 0x4;
			hasDirtyFields = true;
		}

		private void RunChange_hp(ulong timestep)
		{
			if (hpChanged != null) hpChanged(_hp, timestep);
			if (fieldAltered != null) fieldAltered("hp", _hp, timestep);
		}
		private int _hpMax;
		public event FieldEvent<int> hpMaxChanged;
		public Interpolated<int> hpMaxInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int hpMax
		{
			get { return _hpMax; }
			set
			{
				// Don't do anything if the value is the same
				if (_hpMax == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x8;
				_hpMax = value;
				hasDirtyFields = true;
			}
		}

		public void SethpMaxDirty()
		{
			_dirtyFields[0] |= 0x8;
			hasDirtyFields = true;
		}

		private void RunChange_hpMax(ulong timestep)
		{
			if (hpMaxChanged != null) hpMaxChanged(_hpMax, timestep);
			if (fieldAltered != null) fieldAltered("hpMax", _hpMax, timestep);
		}
		private int _stamina;
		public event FieldEvent<int> staminaChanged;
		public Interpolated<int> staminaInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int stamina
		{
			get { return _stamina; }
			set
			{
				// Don't do anything if the value is the same
				if (_stamina == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x10;
				_stamina = value;
				hasDirtyFields = true;
			}
		}

		public void SetstaminaDirty()
		{
			_dirtyFields[0] |= 0x10;
			hasDirtyFields = true;
		}

		private void RunChange_stamina(ulong timestep)
		{
			if (staminaChanged != null) staminaChanged(_stamina, timestep);
			if (fieldAltered != null) fieldAltered("stamina", _stamina, timestep);
		}
		private int _staminaMax;
		public event FieldEvent<int> staminaMaxChanged;
		public Interpolated<int> staminaMaxInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int staminaMax
		{
			get { return _staminaMax; }
			set
			{
				// Don't do anything if the value is the same
				if (_staminaMax == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x20;
				_staminaMax = value;
				hasDirtyFields = true;
			}
		}

		public void SetstaminaMaxDirty()
		{
			_dirtyFields[0] |= 0x20;
			hasDirtyFields = true;
		}

		private void RunChange_staminaMax(ulong timestep)
		{
			if (staminaMaxChanged != null) staminaMaxChanged(_staminaMax, timestep);
			if (fieldAltered != null) fieldAltered("staminaMax", _staminaMax, timestep);
		}
		private uint _playerNetworkId;
		public event FieldEvent<uint> playerNetworkIdChanged;
		public Interpolated<uint> playerNetworkIdInterpolation = new Interpolated<uint>() { LerpT = 0f, Enabled = false };
		public uint playerNetworkId
		{
			get { return _playerNetworkId; }
			set
			{
				// Don't do anything if the value is the same
				if (_playerNetworkId == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x40;
				_playerNetworkId = value;
				hasDirtyFields = true;
			}
		}

		public void SetplayerNetworkIdDirty()
		{
			_dirtyFields[0] |= 0x40;
			hasDirtyFields = true;
		}

		private void RunChange_playerNetworkId(ulong timestep)
		{
			if (playerNetworkIdChanged != null) playerNetworkIdChanged(_playerNetworkId, timestep);
			if (fieldAltered != null) fieldAltered("playerNetworkId", _playerNetworkId, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			positionInterpolation.current = positionInterpolation.target;
			rotationInterpolation.current = rotationInterpolation.target;
			hpInterpolation.current = hpInterpolation.target;
			hpMaxInterpolation.current = hpMaxInterpolation.target;
			staminaInterpolation.current = staminaInterpolation.target;
			staminaMaxInterpolation.current = staminaMaxInterpolation.target;
			playerNetworkIdInterpolation.current = playerNetworkIdInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _position);
			UnityObjectMapper.Instance.MapBytes(data, _rotation);
			UnityObjectMapper.Instance.MapBytes(data, _hp);
			UnityObjectMapper.Instance.MapBytes(data, _hpMax);
			UnityObjectMapper.Instance.MapBytes(data, _stamina);
			UnityObjectMapper.Instance.MapBytes(data, _staminaMax);
			UnityObjectMapper.Instance.MapBytes(data, _playerNetworkId);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_position = UnityObjectMapper.Instance.Map<Vector3>(payload);
			positionInterpolation.current = _position;
			positionInterpolation.target = _position;
			RunChange_position(timestep);
			_rotation = UnityObjectMapper.Instance.Map<Quaternion>(payload);
			rotationInterpolation.current = _rotation;
			rotationInterpolation.target = _rotation;
			RunChange_rotation(timestep);
			_hp = UnityObjectMapper.Instance.Map<int>(payload);
			hpInterpolation.current = _hp;
			hpInterpolation.target = _hp;
			RunChange_hp(timestep);
			_hpMax = UnityObjectMapper.Instance.Map<int>(payload);
			hpMaxInterpolation.current = _hpMax;
			hpMaxInterpolation.target = _hpMax;
			RunChange_hpMax(timestep);
			_stamina = UnityObjectMapper.Instance.Map<int>(payload);
			staminaInterpolation.current = _stamina;
			staminaInterpolation.target = _stamina;
			RunChange_stamina(timestep);
			_staminaMax = UnityObjectMapper.Instance.Map<int>(payload);
			staminaMaxInterpolation.current = _staminaMax;
			staminaMaxInterpolation.target = _staminaMax;
			RunChange_staminaMax(timestep);
			_playerNetworkId = UnityObjectMapper.Instance.Map<uint>(payload);
			playerNetworkIdInterpolation.current = _playerNetworkId;
			playerNetworkIdInterpolation.target = _playerNetworkId;
			RunChange_playerNetworkId(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _position);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _rotation);
			if ((0x4 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _hp);
			if ((0x8 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _hpMax);
			if ((0x10 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _stamina);
			if ((0x20 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _staminaMax);
			if ((0x40 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _playerNetworkId);

			return dirtyFieldsData;
		}

		protected override void ReadDirtyFields(BMSByte data, ulong timestep)
		{
			if (readDirtyFlags == null)
				Initialize();

			Buffer.BlockCopy(data.byteArr, data.StartIndex(), readDirtyFlags, 0, readDirtyFlags.Length);
			data.MoveStartIndex(readDirtyFlags.Length);

			if ((0x1 & readDirtyFlags[0]) != 0)
			{
				if (positionInterpolation.Enabled)
				{
					positionInterpolation.target = UnityObjectMapper.Instance.Map<Vector3>(data);
					positionInterpolation.Timestep = timestep;
				}
				else
				{
					_position = UnityObjectMapper.Instance.Map<Vector3>(data);
					RunChange_position(timestep);
				}
			}
			if ((0x2 & readDirtyFlags[0]) != 0)
			{
				if (rotationInterpolation.Enabled)
				{
					rotationInterpolation.target = UnityObjectMapper.Instance.Map<Quaternion>(data);
					rotationInterpolation.Timestep = timestep;
				}
				else
				{
					_rotation = UnityObjectMapper.Instance.Map<Quaternion>(data);
					RunChange_rotation(timestep);
				}
			}
			if ((0x4 & readDirtyFlags[0]) != 0)
			{
				if (hpInterpolation.Enabled)
				{
					hpInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					hpInterpolation.Timestep = timestep;
				}
				else
				{
					_hp = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_hp(timestep);
				}
			}
			if ((0x8 & readDirtyFlags[0]) != 0)
			{
				if (hpMaxInterpolation.Enabled)
				{
					hpMaxInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					hpMaxInterpolation.Timestep = timestep;
				}
				else
				{
					_hpMax = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_hpMax(timestep);
				}
			}
			if ((0x10 & readDirtyFlags[0]) != 0)
			{
				if (staminaInterpolation.Enabled)
				{
					staminaInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					staminaInterpolation.Timestep = timestep;
				}
				else
				{
					_stamina = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_stamina(timestep);
				}
			}
			if ((0x20 & readDirtyFlags[0]) != 0)
			{
				if (staminaMaxInterpolation.Enabled)
				{
					staminaMaxInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					staminaMaxInterpolation.Timestep = timestep;
				}
				else
				{
					_staminaMax = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_staminaMax(timestep);
				}
			}
			if ((0x40 & readDirtyFlags[0]) != 0)
			{
				if (playerNetworkIdInterpolation.Enabled)
				{
					playerNetworkIdInterpolation.target = UnityObjectMapper.Instance.Map<uint>(data);
					playerNetworkIdInterpolation.Timestep = timestep;
				}
				else
				{
					_playerNetworkId = UnityObjectMapper.Instance.Map<uint>(data);
					RunChange_playerNetworkId(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (positionInterpolation.Enabled && !positionInterpolation.current.UnityNear(positionInterpolation.target, 0.0015f))
			{
				_position = (Vector3)positionInterpolation.Interpolate();
				//RunChange_position(positionInterpolation.Timestep);
			}
			if (rotationInterpolation.Enabled && !rotationInterpolation.current.UnityNear(rotationInterpolation.target, 0.0015f))
			{
				_rotation = (Quaternion)rotationInterpolation.Interpolate();
				//RunChange_rotation(rotationInterpolation.Timestep);
			}
			if (hpInterpolation.Enabled && !hpInterpolation.current.UnityNear(hpInterpolation.target, 0.0015f))
			{
				_hp = (int)hpInterpolation.Interpolate();
				//RunChange_hp(hpInterpolation.Timestep);
			}
			if (hpMaxInterpolation.Enabled && !hpMaxInterpolation.current.UnityNear(hpMaxInterpolation.target, 0.0015f))
			{
				_hpMax = (int)hpMaxInterpolation.Interpolate();
				//RunChange_hpMax(hpMaxInterpolation.Timestep);
			}
			if (staminaInterpolation.Enabled && !staminaInterpolation.current.UnityNear(staminaInterpolation.target, 0.0015f))
			{
				_stamina = (int)staminaInterpolation.Interpolate();
				//RunChange_stamina(staminaInterpolation.Timestep);
			}
			if (staminaMaxInterpolation.Enabled && !staminaMaxInterpolation.current.UnityNear(staminaMaxInterpolation.target, 0.0015f))
			{
				_staminaMax = (int)staminaMaxInterpolation.Interpolate();
				//RunChange_staminaMax(staminaMaxInterpolation.Timestep);
			}
			if (playerNetworkIdInterpolation.Enabled && !playerNetworkIdInterpolation.current.UnityNear(playerNetworkIdInterpolation.target, 0.0015f))
			{
				_playerNetworkId = (uint)playerNetworkIdInterpolation.Interpolate();
				//RunChange_playerNetworkId(playerNetworkIdInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public PlayerNetworkObject() : base() { Initialize(); }
		public PlayerNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public PlayerNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TgSharp.TL;

namespace TgSharp.TL.Stickers
{
    [TLObject(-251435136)]
    public class TLRequestCreateStickerSet : TLMethod<Messages.TLStickerSet>
    {
        public override int Constructor
        {
            get
            {
                return -251435136;
            }
        }

        public int Flags { get; set; }
        public bool Masks { get; set; }
        public bool Animated { get; set; }
        public TLAbsInputUser UserId { get; set; }
        public string Title { get; set; }
        public string ShortName { get; set; }
        public TLAbsInputDocument Thumb { get; set; }
        public TLVector<TLInputStickerSetItem> Stickers { get; set; }
        

        public void ComputeFlags()
        {
            Flags = 0;
Flags = Masks ? (Flags | 1) : (Flags & ~1);
Flags = Animated ? (Flags | 2) : (Flags & ~2);
Flags = Thumb != null ? (Flags | 4) : (Flags & ~4);

        }

        public override void DeserializeBody(BinaryReader br)
        {
            Flags = br.ReadInt32();
            Masks = (Flags & 1) != 0;
            Animated = (Flags & 2) != 0;
            UserId = (TLAbsInputUser)ObjectUtils.DeserializeObject(br);
            Title = StringUtil.Deserialize(br);
            ShortName = StringUtil.Deserialize(br);
            if ((Flags & 4) != 0)
                Thumb = (TLAbsInputDocument)ObjectUtils.DeserializeObject(br);
            else
                Thumb = null;

            Stickers = (TLVector<TLInputStickerSetItem>)ObjectUtils.DeserializeVector<TLInputStickerSetItem>(br);
        }

        public override void SerializeBody(BinaryWriter bw)
        {
            bw.Write(Constructor);
            bw.Write(Flags);
            ObjectUtils.SerializeObject(UserId, bw);
            StringUtil.Serialize(Title, bw);
            StringUtil.Serialize(ShortName, bw);
            if ((Flags & 4) != 0)
                ObjectUtils.SerializeObject(Thumb, bw);
            ObjectUtils.SerializeObject(Stickers, bw);
        }

        protected override void DeserializeResponse(BinaryReader br)
        {
            Response = (Messages.TLStickerSet)ObjectUtils.DeserializeObject(br);
        }
    }
}

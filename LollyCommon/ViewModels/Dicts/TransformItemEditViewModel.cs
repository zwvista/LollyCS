using ReactiveUI;

namespace LollyCommon
{
    public partial class TransformItemEditViewModel : ReactiveObject
    {
        MTransformItem item;
        public MTransformItemEdit ItemEdit = new();

        public TransformItemEditViewModel(MTransformItem item)
        {
            this.item = item;
            item.CopyProperties(ItemEdit);
        }
        public void OnOK()
        {
            ItemEdit.CopyProperties(item);
        }
    }
}

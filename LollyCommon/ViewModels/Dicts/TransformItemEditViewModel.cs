using ReactiveUI;

namespace LollyCommon
{
    public class TransformItemEditViewModel : ReactiveObject
    {
        MTransformItem item;
        public MTransformItemEdit ItemEdit = new MTransformItemEdit();

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

using ReactiveUI;

namespace LollyCloud
{
    public class TransformItemEditViewModel : ReactiveObject
    {
        MTransformItem item;
        public MTransformItem ItemEdit = new MTransformItem();

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

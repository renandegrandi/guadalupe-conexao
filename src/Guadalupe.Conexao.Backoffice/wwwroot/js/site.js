// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$('.custom-file-upload').each(function (index, item) {

    var $item = $(item);

    var $preview = $item.find('.file-upload-preview');
    var $previewPrepend = $preview.find('div');
    var $newFile = $item.find('.file-upload-new');
    var $inputFile = $item.find('input[type=\'file\']');
    var $btnTrocar = $preview.find('button');

    var $imagem = $preview.find('img');

    if (!!$imagem.length) {
        $preview.css('display', 'flex');
        $newFile.css('display', 'none');
    }
    else {
        $preview.css('display', 'none');
        $newFile.css('display', 'block');
    }

    $inputFile.on('change', function (e) {

        e.stopPropagation();

        var files = e.target.files === undefined ? (e.target && e.target.value ? [{ name: e.target.value.replace(/^.+\\/, '') }] : []) : e.target.files;

        if (!files.length) {
            $preview.css('display', 'none');
            $newFile.css('display', 'block');
        }
        else {
            $imagem = $preview.find('img');

            let file = files[0];

            $preview.css('display', 'flex');
            $newFile.css('display', 'none');
            $preview.find('span.nome').text(file.name);

            if (!$imagem.length) {
                $imagem = $('<img>');
            }
            else {
                $imagem.remove();
            }

            var reader = new FileReader();

            reader.onload = function (re) {
                $imagem[0].src = re.target.result
                $imagem[0].result = re.target.result

                $previewPrepend.prepend($imagem);
            }

            reader.readAsDataURL(file)
        }

    });

    $btnTrocar.on('click', function () {
        $inputFile.click();
    });
});
#Exemplo de uma aplicação do tipo Blog

Script da base de dados:

CREATE TABLE `usuario` (`id` INTEGER PRIMARY KEY, `nome` TEXT, `email` TEXT, `hash_senha` TEXT, `slug` TEXT);
CREATE TABLE `postagem` (`id` INTEGER PRIMARY KEY, `id_autor` INTEGER, `titulo` TEXT, `conteudo` TEXT, `data_hora` TEXT, `likes` INTEGER, deslikes INTEGER);
CREATE TABLE `postagem_reacao` (`id` INTEGER PRIMARY KEY, `id_postagem` INTEGER, `id_usuario` INTEGER, `reacao` INTEGER, `data_hora` TEXT);

